using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace TenmoClient.Data
{
    public class Transfers
    {
        public int transfer_id { get; set; }

        public TransferType transfer_type_id { get; set; }

        public TransferStatus transfer_status_id { get; set; }

        public decimal Amount { get; set; }

        public int account_from { get; set; }

        public int account_to { get; set; }
    }

    public class VTransfers
    {
        public int transfer_id { get; set; }

        public FromTo transfer_type_id { get; set; }

        public TransferStatus transfer_status_id { get; set; }

        public decimal Amount { get; set; }

        public int account_from { get; set; }

        public int account_to { get; set; }
    }

    public enum TransferStatus : int
    {
        Pending = 1,
        Approved,
        Rejected
    }

    public enum TransferType : int
    {
        Request = 1,
        Send
    }

    public enum FromTo : int
    {
        From = 1,
        To
    }
}
