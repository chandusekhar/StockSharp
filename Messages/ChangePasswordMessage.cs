namespace StockSharp.Messages
{
	using System;
	using System.Runtime.Serialization;
	using System.Security;
	using System.Xml.Serialization;

	/// <summary>
	/// Change password message.
	/// </summary>
	[DataContract]
	[Serializable]
	public class ChangePasswordMessage :
		BaseResultMessage<ChangePasswordMessage>, ITransactionIdMessage, IErrorMessage
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ChangePasswordMessage"/>.
		/// </summary>
		public ChangePasswordMessage()
			: base(MessageTypes.ChangePassword)
		{
		}

		/// <inheritdoc />
		[DataMember]
		public long TransactionId { get; set; }

		/// <inheritdoc />
		[DataMember]
		[XmlIgnore]
		public Exception Error { get; set; }

		[field: NonSerialized]
		private SecureString _newPassword;

		/// <summary>
		/// New password.
		/// </summary>
		[DataMember]
		public SecureString NewPassword
		{
			get => _newPassword;
			set => _newPassword = value;
		}

		/// <summary>
		/// User name.
		/// </summary>
		[DataMember]
		public string UserName { get; set; }

		[field: NonSerialized]
		private SecureString _oldPassword;

		/// <summary>
		/// Old password.
		/// </summary>
		[DataMember]
		public SecureString OldPassword
		{
			get => _oldPassword;
			set => _oldPassword = value;
		}

		/// <inheritdoc />
		protected override void CopyTo(ChangePasswordMessage destination)
		{
			base.CopyTo(destination);

			destination.TransactionId = TransactionId;
			destination.UserName = UserName;
			destination.OldPassword = OldPassword;
			destination.NewPassword = NewPassword;
			destination.Error = Error;
		}

		/// <inheritdoc />
		public override string ToString()
		{
			var str = base.ToString();

			if (Error != null)
				str += $",Error={Error.Message}";

			return str;
		}
	}
}