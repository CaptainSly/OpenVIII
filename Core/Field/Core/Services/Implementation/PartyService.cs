﻿using System;

namespace OpenVIII
{
    public sealed class PartyService : IPartyService
    {
        public Boolean IsSupported => true;
        public Boolean IsPartySwitchEnabled { get; set; }

        public void AddPlayableCharacter(Characters characterId)
        {
            // TODO: Field script
            Console.WriteLine($"NotImplemented: {nameof(PartyService)}.{nameof(AddPlayableCharacter)}({nameof(characterId)}: {characterId})");
        }

        public void RemovePlayableCharacter(Characters characterId)
        {
            // TODO: Field script
            Console.WriteLine($"NotImplemented: {nameof(PartyService)}.{nameof(RemovePlayableCharacter)}({nameof(characterId)}: {characterId})");
        }

        public void AddPartyCharacter(Characters characterId)
        {
            // TODO: Field script
            Console.WriteLine($"NotImplemented: {nameof(PartyService)}.{nameof(AddPartyCharacter)}({nameof(characterId)}: {characterId})");
        }

        public void RemovePartyCharacter(Characters characterId)
        {
            // TODO: Field script
            Console.WriteLine($"NotImplemented: {nameof(PartyService)}.{nameof(RemovePartyCharacter)}({nameof(characterId)}: {characterId})");
        }

        public void ChangeCharacterState(Characters characterId, Boolean isSwitchable, Boolean isSelectable)
        {
            // TODO: Field script
            Console.WriteLine($"NotImplemented: {nameof(PartyService)}.{nameof(ChangeCharacterState)}({nameof(characterId)}: {characterId}, {nameof(isSwitchable)}: {isSwitchable}, {nameof(isSelectable)}: {isSelectable})");
        }

        public void ChangeParty(Characters characterId1, Characters characterId2, Characters characterId3)
        {
            // TODO: Field script
            Console.WriteLine($"NotImplemented: {nameof(PartyService)}.{nameof(ChangeParty)}({nameof(characterId1)}: {characterId1}, {nameof(characterId2)}: {characterId2}, {nameof(characterId3)}: {characterId3})");
        }

        public FieldObject FindPartyCharacterObject(Int32 partyId)
        {
            // TODO: Field script
            Console.WriteLine($"NotImplemented: {nameof(PartyService)}.{nameof(FindPartyCharacterObject)}({nameof(partyId)}: {partyId})");

            return null;
        }
    }
}