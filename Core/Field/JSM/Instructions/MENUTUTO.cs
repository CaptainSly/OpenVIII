﻿using System;


namespace OpenVIII.Fields.Scripts.Instructions
{
    internal sealed class MENUTUTO : JsmInstruction
    {
        public MENUTUTO()
        {
        }

        public MENUTUTO(Int32 parameter, IStack<IJsmExpression> stack)
            : this()
        {
        }

        public override String ToString()
        {
            return $"{nameof(MENUTUTO)}()";
        }
    }
}