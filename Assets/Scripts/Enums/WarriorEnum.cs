public enum Race {
    None, Construct, Dragon, Support, //Common
    Ghoul, Lich, Skeleton, Vampire, Wraith, Zombie, Nightrider, Shade, Reaper, //Undead
    Pirate, Holyborn, Knight, Griffin, Fencer, Librarian, Farmer, Soldier, Marksman, Engineer, //Human
    Unicorn, Ranger, Dwarf, Centaur, Troll, Treant, Werewolf, Pixie, Sorcerer, //Elves
    Imp, Minotaur, Harpy, Pestilence, Cerberus, Succubus, Demon, Hydra, //Underworld
    Dark, Fire, Light, Nature //Spells
}

public enum Genre {
    None, Human, Elves, Undead, Underworld
}

public enum Direction {
    Left, Right
};

public enum DamageType {
    Physical, Magical
};

public enum Alignment {
    None, Enemy, Friend
};

public enum DamageSource {
    Normal, Burning, Poisoned
}