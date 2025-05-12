namespace DeviceManager.BRICK32
{
    public enum STAR_EOP_TYPE
    {
        /** Error occurred determining EOP type */
        STAR_EOP_TYPE_INVALID,
        /** End of Packet marker*/
        STAR_EOP_TYPE_EOP,
        /** Error End of Packet marker*/
        STAR_EOP_TYPE_EEP,
        /** No End of Packet marker present */
        STAR_EOP_TYPE_NONE
    }
}
