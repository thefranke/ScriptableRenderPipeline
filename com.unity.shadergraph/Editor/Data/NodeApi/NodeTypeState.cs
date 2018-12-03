using System.Collections.Generic;
using System.Linq;

namespace UnityEditor.ShaderGraph
{
    abstract class NodeTypeState
    {
        public int id;
        public AbstractMaterialGraph owner;
        public NodeTypeDescriptor type;
        public bool typeCreated;
        public List<InputPortDescriptor> inputPorts = new List<InputPortDescriptor>();
        public List<OutputPortDescriptor> outputPorts = new List<OutputPortDescriptor>();
        public List<HlslSource> hlslSources = new List<HlslSource>();
        public List<ControlState> controls = new List<ControlState>();
        public List<HlslValue> hlslValues = new List<HlslValue>();

        #region Change lists for consumption by IShaderNode implementation

        // TODO: Need to also store node ID versions somewhere
        public IndexSet addedNodes = new IndexSet();
        public IndexSet modifiedNodes = new IndexSet();

        #endregion

        public bool isDirty => addedNodes.Any() || modifiedNodes.Any();

        public void ClearChanges()
        {
            addedNodes.Clear();
            modifiedNodes.Clear();
            // TODO: Use IndexSet for modified controls
            for (var i = 0; i < controls.Count; i++)
            {
                var control = controls[i];
                control.wasModified = false;
                controls[i] = control;
            }
        }

        ShaderNodeType m_NodeType;

        public ShaderNodeType nodeType
        {
            get => m_NodeType;
            set
            {
                SetNodeType(value);
                m_NodeType = value;
            }
        }

        protected abstract void SetNodeType(ShaderNodeType value);

        public abstract void DispatchChanges(NodeChangeContext context);
    }

    // This construction allows us to move the virtual call to outside the loop. The calls to the ShaderNodeType in
    // DispatchChanges are to a generic type parameter, and thus will be devirtualized if T is a sealed class.
    sealed class NodeTypeState<T> : NodeTypeState where T : ShaderNodeType
    {
        public new T nodeType { get; set; }

        protected override void SetNodeType(ShaderNodeType value)
        {
            nodeType = (T)value;
        }

        public override void DispatchChanges(NodeChangeContext context)
        {
            foreach (var node in addedNodes)
            {
                nodeType.OnNodeAdded(context, new ShaderNode(owner, owner.currentStateId, (ProxyShaderNode)owner.m_Nodes[node]));
            }

            foreach (var node in modifiedNodes)
            {
                nodeType.OnNodeModified(context, new ShaderNode(owner, owner.currentStateId, (ProxyShaderNode)owner.m_Nodes[node]));
            }
        }
    }
}
