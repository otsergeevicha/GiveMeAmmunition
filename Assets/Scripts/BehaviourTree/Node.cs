using System.Collections.Generic;

namespace BehaviourTree
{
    public enum NodeState
    {
        Running,
        Success,
        Failure
    }
    
    public class Node
    {
        protected NodeState State;

        public Node Parent;
        protected List<Node> Childrens = new ();

        private Dictionary<string, object> _dataContext = new ();

        public Node() => 
            Parent = null;

        public Node(List<Node> children)
        {
            foreach (Node child in children) 
                Attach(child);
        }

        private void Attach(Node node)
        {
            node.Parent = this;
            Childrens.Add(node);
        }

        public virtual NodeState Evaluate() => 
            NodeState.Failure;

        public void SetData(string key, object value) => 
            _dataContext[key] = value;

        public object GetData(string key)
        {
            object value = null;

            if (_dataContext.TryGetValue(key, out value))
                return value;

            Node node = Parent;

            while (node != null)
            {
                value = node.GetData(key);

                if (value!= null)
                    return value;

                node = node.Parent;
            }

            return null;
        }

        public bool ClearData(string key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }

            Node node = Parent;

            while (node != null)
            {
                bool cleared = node.ClearData(key);
                
                if (cleared)
                    return true;

                node = node.Parent;
            }

            return false;
        }
    }
}