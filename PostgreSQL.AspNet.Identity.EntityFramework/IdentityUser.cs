using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;

namespace PostgreSQL.AspNet.Identity.EntityFramework {

	/// <summary>
	/// Default EntityFramework IUser implementation
	/// </summary>
	public class IdentityUser : IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>, IUser {

		/// <summary>
		/// Constructor which creates a new Guid for the Id
		/// </summary>
		public IdentityUser() {
			Id = Guid.NewGuid().ToString();
		}

		/// <summary>
		/// Constructor that takes a userName
		/// </summary>
		/// <param name="userName"></param>
		public IdentityUser(string userName)
			: this() {
			UserName = userName;
		}
	}

	/// <summary>
	/// Default EntityFramework IUser implementation
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TLogin"></typeparam>
	/// <typeparam name="TRole"></typeparam>
	/// <typeparam name="TClaim"></typeparam>
	public class IdentityUser<TKey, TLogin, TRole, TClaim> : IUser<TKey>
		where TLogin : IdentityUserLogin<TKey>
		where TRole : IdentityUserRole<TKey>
		where TClaim : IdentityUserClaim<TKey> {

		/// <summary>
		/// Constructor
		/// </summary>
		public IdentityUser() {
			Claims = new List<TClaim>();
			Roles = new List<TRole>();
			Logins = new List<TLogin>();
		}

		/// <summary>
		/// Used to record failures for the purposes of lockout
		/// </summary>
		public virtual int AccessFailedCount { get; set; }

		/// <summary>
		/// Navigation property for user claims
		/// </summary>
		public virtual ICollection<TClaim> Claims { get; private set; }

		/// <summary>
		/// Email
		/// </summary>
		public virtual string Email { get; set; }

		/// <summary>
		/// True if the email is confirmed, default is false
		/// </summary>
		public virtual bool EmailConfirmed { get; set; }

		/// <summary>
		/// User ID (Primary Key)
		/// </summary>
		public virtual TKey Id { get; set; }

		/// <summary>
		/// Is lockout enabled for this user
		/// </summary>
		public virtual bool LockoutEnabled { get; set; }

		/// <summary>
		/// DateTime in UTC when lockout ends, any time in the past is considered not locked out.
		/// </summary>
		public virtual DateTime? LockoutEndDateUtc { get; set; }

		/// <summary>
		/// Navigation property for user logins
		/// </summary>
		public virtual ICollection<TLogin> Logins { get; private set; }

		/// <summary>
		/// The salted/hashed form of the user password
		/// </summary>
		public virtual string PasswordHash { get; set; }

		/// <summary>
		/// PhoneNumber for the user
		/// </summary>
		public virtual string PhoneNumber { get; set; }

		/// <summary>
		/// True if the phone number is confirmed, default is false
		/// </summary>
		public virtual bool PhoneNumberConfirmed { get; set; }

		/// <summary>
		/// Navigation property for user roles
		/// </summary>
		public virtual ICollection<TRole> Roles { get; private set; }

		/// <summary>
		/// A random value that should change whenever a users credentials have changed (password
		/// changed, login removed)
		/// </summary>
		public virtual string SecurityStamp { get; set; }

		/// <summary>
		/// Is two factor enabled for the user
		/// </summary>
		public virtual bool TwoFactorEnabled { get; set; }

		/// <summary>
		/// User name
		/// </summary>
		public virtual string UserName { get; set; }
	}
}
