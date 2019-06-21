﻿using System;


namespace OpenVIII
{
    internal sealed class FOLLOWON : JsmInstruction
    {
        public FOLLOWON()
        {
        }

        public FOLLOWON(Int32 parameter, IStack<IJsmExpression> stack)
            : this()
        {
        }

        public override String ToString()
        {
            return $"{nameof(FOLLOWON)}()";
        }
    }
}