using System;


namespace OpenVIII.Fields.Scripts.Instructions
{
    internal sealed class KEYSIGHNCHANGE : JsmInstruction
    {
        private IJsmExpression _arg0;

        public KEYSIGHNCHANGE(IJsmExpression arg0)
        {
            _arg0 = arg0;
        }

        public KEYSIGHNCHANGE(Int32 parameter, IStack<IJsmExpression> stack)
            : this(
                arg0: stack.Pop())
        {
        }

        public override String ToString()
        {
            return $"{nameof(KEYSIGHNCHANGE)}({nameof(_arg0)}: {_arg0})";
        }
    }
}