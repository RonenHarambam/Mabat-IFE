namespace DeviceManager.BRICK32
{
    public enum STAR_CHANNEL_DIRECTION
    {
        /** Channel may be used to receive traffic */
        STAR_CHANNEL_DIRECTION_IN = 1,
        /** Channel may be used to transmit traffic */
        STAR_CHANNEL_DIRECTION_OUT = 2,
        /** Channel may be used to both receive and transmit traffic */
        STAR_CHANNEL_DIRECTION_INOUT = STAR_CHANNEL_DIRECTION_IN |
                                          STAR_CHANNEL_DIRECTION_OUT
    }
}
