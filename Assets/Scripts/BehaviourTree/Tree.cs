using Plugins.MonoCache;

namespace BehaviourTree
{
    public abstract class Tree : MonoCache
    {
        private Node _root = null;

        protected void Start() => 
            _root = SetupTree();

        protected override void UpdateCached()
        {
            if (_root!=null) 
                _root.Evaluate();
        }

        protected abstract Node SetupTree();
    }
}