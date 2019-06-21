﻿using System;


namespace OpenVIII
{
    internal sealed class KEYSCAN : JsmInstruction
    {
        private IJsmExpression _arg0;

        public KEYSCAN(IJsmExpression arg0)
        {
            _arg0 = arg0;
        }

        public KEYSCAN(Int32 parameter, IStack<IJsmExpression> stack)
            : this(
                arg0: stack.Pop())
        {
        }

        public override String ToString()
        {
            return $"{nameof(KEYSCAN)}({nameof(_arg0)}: {_arg0})";
        }
    }
}