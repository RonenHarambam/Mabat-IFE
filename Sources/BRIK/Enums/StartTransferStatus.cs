namespace DeviceManager.BRICK32
{
    public enum STAR_TRANSFER_STATUS
    {
        /**
         * Not yet started.  When a transfer operation is created, this will be its
         * status.
         */
        STAR_TRANSFER_STATUS_NOT_STARTED,

        /**
         * Transfer has begun.  When a transfer operation is submitted, this will be
         * its status, until it has completed. */
        STAR_TRANSFER_STATUS_STARTED,

        /**
         * Transfer has completed.  Once a transmit operation has successfully
         * transmitted all its traffic, or a receive operation has received all its
         * requested traffic, this will be its status.
         */
        STAR_TRANSFER_STATUS_COMPLETE,

        /**
         * Transfer was cancelled.  When a transfer is cancelled by calling
         * STAR_cancelTransferOperation(), this will be its status.
         */
        STAR_TRANSFER_STATUS_CANCELLED,

        /**
         * An error occurred while processing the transfer.  This will be the status
         * of a transfer operation if there was an error creating it, submitting it,
         * or there was an error while transmitting or receiving.
         */
        STAR_TRANSFER_STATUS_ERROR
    }
}
