using System.Collections.Generic;
using TenmoClient.Data;

namespace TenmoServer.DAO
{
    public interface ITransferDAO
    {
        bool UpdateBalance(int userId, decimal updatedBalance);

        bool LogTransfers(TransferType transfer_type_id, TransferStatus transfer_status_id, int accountID_from, int accountID_to, decimal amount);

        List<Transfers> ViewTransfers(int id);

        Transfers GetTransfer(int id);
        List<Transfers> ViewPendingTransfers(int user_id);
        bool UpdateStatus(int transferId, int statusId);

    }
}