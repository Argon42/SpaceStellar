using System;
using R3;

namespace Bananva.Utilities.DataSource
{
    /// <summary>
    /// <para> Класс, содержащий в себе общее поведение всех <see cref="IAsyncDataSource"/> </para>
    /// <para> В основном используется через методы расширения в <see cref="DataSourcesUtils"/> </para>
    /// </summary>
    public class DataSourceBehaviour
    {
        private readonly Action m_onPostReset = delegate { };
        private readonly ReactiveProperty<bool> m_dataReady = new(false);

        /// <summary>
        /// <para> Является ли источник данных <b>сбрасываемым</b>, т.е должен ли вызываться <see cref="Reset"/> при входе в мету. </para>
        /// <para> Актуально для тех источников данных, которые хранятся на протяжении всей сессии. </para>
        /// </summary>
        public bool Resettable { get; }

        /// <summary>
        /// <para> Реактивное свойство, сообщающее о готовности источника данных. </para>
        /// </summary>
        public ReadOnlyReactiveProperty<bool> DataReady => m_dataReady;

        /// <summary>
        /// <para> Конструктор с кастомной логикой сбрасывания. </para>
        /// </summary>
        public DataSourceBehaviour(Action onPostReset, bool enabledByDefault = false)
        {
            m_onPostReset = onPostReset;
            Resettable = true;

            if (enabledByDefault)
            {
                SetReady();
            }
        }

        /// <summary>
        /// <para> Конструктор без кастомной логики сбрасывания. </para> 
        /// </summary>
        public DataSourceBehaviour(bool resettable, bool enabledByDefault = false)
        {
            Resettable = resettable;

            if (enabledByDefault)
            {
                SetReady();
            }
        }

        /// <summary>
        /// <para> Ставит <see cref="DataReady"/> == <c>false</c>, вызывая кастомную логику сброса, если таковая есть. </para>
        /// <para> Игнорирует значение <see cref="Resettable"/>. </para>
        /// </summary>
        public void Reset()
        {
            m_dataReady.Value = false;
            m_onPostReset?.Invoke();
        }

        /// <summary>
        /// <para> Ставит <see cref="DataReady"/> == <c>true</c>. </para>
        /// </summary>
        public void SetReady()
        {
            if (m_dataReady.CurrentValue)
            {
                m_dataReady.ForceNotify();
            }
            else
            {
                m_dataReady.Value = true;
            }
        }
    }
}