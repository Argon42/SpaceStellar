using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Bananva.Utilities.Extensions;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Bananva.Utilities.Tests
{
    public static class TestExtensions
    {
        public static IEnumerable<T> Query<T>(this GameObject gameObject, Predicate<T>? matcher = null)
            where T : class => gameObject.QueryInternal(matcher);

        public static IEnumerable<T> Query<T>(this Component component, Predicate<T>? matcher = null) where T : class =>
            component.gameObject.Query(matcher);

        public static IEnumerable<T> Query<T>(this GameObject gameObject, string name) where T : class
        {
            return gameObject.Query<T>(obj =>
            {
                return obj switch
                {
                    GameObject go => go.name == name,
                    Component component => component.name == name,
                    _ => name == obj.ToString()
                };
            });
        }

        public static IEnumerable<T> Query<T>(this Component component, string name) where T : class =>
            component.gameObject.Query<T>(name);

        public static T? Q<T>(this GameObject gameObject, Predicate<T>? matcher = null) where T : class =>
            gameObject.Query(matcher).FirstOrDefault();

        public static T? Q<T>(this Component component, Predicate<T>? matcher = null) where T : class =>
            component.gameObject.Query(matcher).FirstOrDefault();

        public static T? Q<T>(this GameObject gameObject, string name) where T : class =>
            gameObject.Query<T>(name).FirstOrDefault();

        public static T? Q<T>(this Component component, string name) where T : class =>
            component.gameObject.Query<T>(name).FirstOrDefault();

        private static IEnumerable<T> QueryInternal<T>(this GameObject go, Predicate<T>? matcher) where T : class
        {
            if (go == null)
            {
                return new List<T>();
            }

            if (typeof(T) == typeof(GameObject))
            {
                return go
                    .GetComponentsInChildren<Transform>(includeInactive: true)
                    .Select(child => child.gameObject)
                    .Where(child => matcher == null || matcher(child as T ?? throw new InvalidOperationException()))
                    .Cast<T>();
            }

            return go.GetComponentsInChildren<T>(includeInactive: true)
                .Where(component => matcher == null || matcher(component));
        }

        public static void ClickAtObject(this Component component) =>
            component.ThrowIfArgumentNull().gameObject.ClickAtObject();

        public static void ClickAtObject(this GameObject obj)
        {
            obj.ThrowIfArgumentNull();
            PointerEventData pointer = new(EventSystem.current);

            // 1. Эмулируем наведение курсора на объект
            ExecuteEvents.Execute(obj, pointer, ExecuteEvents.pointerEnterHandler);
            // 2. Эмулируем нажатие кнопки мыши
            ExecuteEvents.Execute(obj, pointer, ExecuteEvents.pointerDownHandler);
            // 3. Эмулируем отпускание кнопки мыши
            ExecuteEvents.Execute(obj, pointer, ExecuteEvents.pointerUpHandler);
            // 4. Эмулируем клик по объекту
            ExecuteEvents.Execute(obj, pointer, ExecuteEvents.pointerClickHandler);
        }

        public static async UniTask DragByObject(this Component component, Vector2 start, Vector2 end, float duration)
            => await component.ThrowIfArgumentNull().gameObject.DragByObject(start, end, duration);

        public static async UniTask DragByObject(this GameObject obj, Vector2 start, Vector2 end, float duration)
        {
            obj.ThrowIfArgumentNull();
            PointerEventData pointer = new(EventSystem.current)
            {
                position = start
            };
            ExecuteEvents.Execute(obj, pointer, ExecuteEvents.pointerDownHandler);

            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                Vector2 currentPos = Vector2.Lerp(start, end, elapsedTime / duration);
                pointer.position = currentPos;

                ExecuteEvents.Execute(obj, pointer, ExecuteEvents.dragHandler);

                await UniTask.Yield(PlayerLoopTiming.Update);
            }

            pointer.position = end;
            ExecuteEvents.Execute(obj, pointer, ExecuteEvents.pointerUpHandler);
        }

        public static void InputAndSend(this GameObject obj, string text)
        {
            TMP_InputField field = obj.Q<TMP_InputField>().ThrowIfArgumentNull();

            field.ClickAtObject();
            field.text = text;
            field.OnSubmit(new PointerEventData(EventSystem.current));
        }

        public static void InputAndSend(this object obj, string text)
        {
            switch (obj)
            {
                case Component component:
                    InputAndSend(component.gameObject, text);
                    break;
                case GameObject go:
                    InputAndSend(go, text);
                    break;
            }
        }

        public static bool IsActiveAndNotNull(this GameObject obj)
        {
            return obj != null && obj.activeSelf && obj.activeInHierarchy;
        }

        [PublicAPI]
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        internal static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            enumerable.ThrowIfArgumentNull();
            action.ThrowIfArgumentNull();

            foreach (T item in enumerable)
            {
                action(item);
            }

            return enumerable;
        }
    }
}