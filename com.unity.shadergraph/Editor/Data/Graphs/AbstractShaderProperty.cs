using System;
using UnityEditor.Graphing;
using UnityEngine;

namespace UnityEditor.ShaderGraph
{
    [Serializable]
    abstract class AbstractShaderProperty : ShaderInput
    {
        public abstract PropertyType propertyType { get; }

        public override ConcreteSlotValueType concreteShaderValueType => propertyType.ToConcreteShaderValueType();

        [SerializeField]
        private Precision m_Precision = Precision.Inherit;
        
        private ConcretePrecision m_ConcretePrecision = ConcretePrecision.Float;

        public Precision precision
        {
            get => m_Precision;
            set => m_Precision = value;
        }

        public ConcretePrecision concretePrecision => m_ConcretePrecision;

        public void SetConcretePrecision(ConcretePrecision inheritedPrecision)
        {
            m_ConcretePrecision = (precision == Precision.Inherit) ? inheritedPrecision : precision.ToConcrete();
        }

        public abstract bool isBatchable { get; }

        [SerializeField]
        bool m_Hidden = false;

        public bool hidden
        {
            get => m_Hidden;
            set => m_Hidden = value;
        }

        public string hideTagString => hidden ? "[HideInInspector]" : "";

        public virtual string GetPropertyBlockString()
        {
            return string.Empty;
        }

        public virtual string GetPropertyDeclarationString(string delimiter = ";")
        {
            SlotValueType type = ConcreteSlotValueType.Vector4.ToSlotValueType();
            return $"{concreteShaderValueType.ToShaderString(concretePrecision.ToShaderString())} {referenceName}{delimiter}";
        }

        public virtual string GetPropertyAsArgumentString()
        {
            return GetPropertyDeclarationString(string.Empty);
        }
        
        public abstract AbstractMaterialNode ToConcreteNode();
        public abstract PreviewProperty GetPreviewMaterialProperty();
    }
    
    [Serializable]
    abstract class AbstractShaderProperty<T> : AbstractShaderProperty
    {
        [SerializeField]
        private T m_Value;

        public T value
        {
            get => m_Value;
            set => m_Value = value;
        }
    }
}
