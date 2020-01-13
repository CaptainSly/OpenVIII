﻿using System;


namespace OpenVIII.Fields.Scripts.Instructions
{
    internal sealed class MUSICVOLSYNC : JsmInstruction
    {
        public MUSICVOLSYNC()
        {
        }

        public MUSICVOLSYNC(Int32 parameter, IStack<IJsmExpression> stack)
            : this()
        {
        }

        public override String ToString()
        {
            return $"{nameof(MUSICVOLSYNC)}()";
        }
    }
}