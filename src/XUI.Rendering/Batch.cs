using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUI.Rendering
{
    public class Batch
    {
        private List<BatchOperation> operations;

        public Batch()
        {
            operations = new List<BatchOperation>();
        }

        public void AddOperation(BatchOperation operation)
        {
            operations.Add(operation);
        }

        public void RemoveOperation(BatchOperation operation)
        {
            operations.Remove(operation);
        }
    }
}
