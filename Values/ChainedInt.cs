using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace parent_house_framework.Values {
    [Serializable]
    public class ChainedInt {
        public ChainedInt(int maxValue, int minValue = 1, bool overflows = true) {
            m_maxValue = maxValue;
            m_minValue = minValue;
            m_overflows = overflows;
            ObservedValue = new ObservableValue<int>();
            Value = m_minValue;
        }

        public ChainedInt Link(ChainedInt chainedInt) {
            m_chainedInt = chainedInt;
            return this;
        }

        private readonly int m_maxValue;
        private readonly int m_minValue;
        private readonly bool m_overflows;
        public ObservableValue<int> ObservedValue { get; }
        private ChainedInt m_chainedInt;

        [SerializeField, BoxGroup("Status")]
        private int m_value;

        public int Value {
            get => ObservedValue.Value;
            private set {
                if (ObservedValue.Value.Equals(value)) return;

                ObservedValue.Value = Math.Clamp(value, m_minValue, m_maxValue);

                // Overflow to chained int if over max
                if (ObservedValue.Value > m_maxValue && m_overflows) {
                    m_chainedInt?.AddValue(1);
                    ObservedValue.Value = m_minValue;
                }
                m_value = ObservedValue.Value;
            }
        }

        public void AddValue(int value) {
            int newValue = Value + value;

            while (newValue > m_maxValue && m_overflows) {
                newValue -= (m_maxValue - m_minValue + 1); // Spread range
                m_chainedInt?.AddValue(1);
            }

            if (newValue > m_maxValue && !m_overflows) {
                newValue = m_maxValue;
            }

            Value = newValue;
        }
    }
}