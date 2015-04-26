using System;

namespace Sharpenguin.Configuration.Game {
    /// <summary>
    /// Enumeration of item types (where they go on the body).
    /// </summary>
    public enum ItemType {
        /// <summary>
        /// Specifies an item that is used as a background.
        /// </summary>
        Background,
        /// <summary>
        /// Specifies an item that is placed on the player's body.
        /// </summary>
        Body,
        /// <summary>
        /// Specifies an item that is used as the player's colour.
        /// </summary>
        Colour,
        /// <summary>
        /// Specifies an item that is placed on the player's face.
        /// </summary>
        Face,
        /// <summary>
        /// Specifies an item that is placed on the player's feet.
        /// </summary>
        Feet,
        /// <summary>
        /// Specifies an item that is used as the player's flag (pin).
        /// </summary>
        Flag,
        /// <summary>
        /// Specifies an item that is placed on the player's hands.
        /// </summary>
        Hand,
        /// <summary>
        /// Specifies an item that is placed on the player's head.
        /// </summary>
        Head,
        /// <summary>
        /// Specifies an item that is placed on the player's neck.
        /// </summary>
        Neck,
        /// <summary>
        /// Specifies an item that does not fit into one of these categories (miscellaneous).
        /// </summary>
        Other,
        /// <summary>
        /// Specifies an item that is not in the configuration (usually happens when the configuration is not up to date).
        /// </summary>
        Unknown
    }
}

