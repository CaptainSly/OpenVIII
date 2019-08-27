﻿using System.Collections.Generic;
using System.IO;

namespace OpenVIII
{
    public partial class Kernel_bin
    {
        /// <summary>
        /// Junction Abilities Data
        /// </summary>
        /// <see cref="https://github.com/alexfilth/doomtrain/wiki/Junction-abilities"/>
        public class Junction_abilities : Ability
        {
            public new const int count = 20;
            public new const int id = 11;

            //public BitArray J_Flags { get; private set; }
            public JunctionAbilityFlags J_Flags { get; private set; }

            public override void Read(BinaryReader br, int i)
            {
                Icon = Icons.ID.Ability_Junction;
                Name = Memory.Strings.Read(Strings.FileID.KERNEL, id, i * 2);
                //0x0000	2 bytes Offset to name
                Description = Memory.Strings.Read(Strings.FileID.KERNEL, id, i * 2 + 1);
                //0x0002	2 bytes Offset to description
                br.BaseStream.Seek(4, SeekOrigin.Current);
                AP = br.ReadByte();
                //0x0004  1 byte AP Required to learn ability
                //J_Flags = new BitArray(br.ReadBytes(3));
                byte[] tmp = br.ReadBytes(3);
                J_Flags = (JunctionAbilityFlags)(tmp[2] << 16 | tmp[1] << 8 | tmp[0]);

                //0x0005  3 byte J_Flag
            }

            public static Dictionary<Abilities,Junction_abilities> Read(BinaryReader br)
            {
                Dictionary<Abilities, Junction_abilities> ret = new Dictionary<Abilities, Junction_abilities>(count);
                
                for (int i = 0; i < count; i++)
                {
                    
                    Junction_abilities tmp = new Junction_abilities();
                    tmp.Read(br, i);
                    ret[(Abilities)i] = tmp;
                }
                return ret;
            }
        }
    }
}