namespace Himy.Models.Accounts
{
    /// <summary>
    /// アカウント処理に関連するデータを格納します。
    /// </summary>
    public class AccountResultData
    {
        /// <summary> 処理の結果を格納します。</summary>
        public bool IsResult;

        /// <summary> メッセージを格納します。</summary>
        public string ResultMessage;

        /// <summary> ログインしているユーザー情報を格納します。</summary>
        public MinUser MinUser;

        /// <summary>
        /// インスタンスの初期化処理です。
        /// </summary>
        public AccountResultData()
        {
            MinUser = new MinUser();
            IsResult = false;
        }
    }
}
