using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.Rendering
{
    public class SpriteBatch
    {
        private List<SpriteBatchOperation> operations;

        public SpriteBatch()
        {
            operations = new List<SpriteBatchOperation>();
        }

        public void AddOperation(SpriteBatchOperation operation)
        {
            operations.Add(operation);
        }

        public void RemoveOperation(SpriteBatchOperation operation)
        {
            operations.Remove(operation);
        }

        public SpriteBatchOperation[] GetOperations()
        {
            return operations.ToArray();
        }
    }
}
