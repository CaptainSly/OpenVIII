﻿using System.Collections.Generic;
using System.IO;

namespace OpenVIII
{
    public partial class Kernel_bin
    {
        /// <summary>
        /// GF Abilities Data
        /// </summary>
        /// <see cref="https://github.com/alexfilth/doomtrain/wiki/GF-abilities"/>
        public class GF_abilities : Equipable_Ability
        {
            public new const int count = 9;
            public new const int id = 16;

            public byte Boost { get; private set; }
            public Stat Stat { get; private set; }
            public byte Value { get; private set; }

            public override void Read(BinaryReader br, int i)
            {
                Icon = Icons.ID.Ability_GF;
                Name = Memory.Strings.Read(Strings.FileID.KERNEL, id, i * 2);
                //0x0000	2 bytes Offset to name
                Description = Memory.Strings.Read(Strings.FileID.KERNEL, id, i * 2 + 1);
                //0x0002	2 bytes Offset to description
                br.BaseStream.Seek(4, SeekOrigin.Current);
                AP = br.ReadByte();
                //0x0004 1 byte AP needed to learn the ability
                Boost = br.ReadByte();
                //0x0005 Enable Boost
                Stat = (Stat)br.ReadByte();
                //0x0006  1 byte Stat to increase
                Value = br.ReadByte();
                //0x0007  1 byte Increase value
            }
            public static Dictionary<Abilities, GF_abilities> Read(BinaryReader br)
            {
                Dictionary<Abilities, GF_abilities> ret = new Dictionary<Abilities, GF_abilities>(count);

                for (int i = 0; i < count; i++)
                {
                    var tmp = new GF_abilities();
                    tmp.Read(br, i);
                    ret[(Abilities)(i+(int)Abilities.SumMag_10)] = tmp;
                }
                return ret;
            }
        }
    }
}