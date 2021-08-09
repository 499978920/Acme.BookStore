using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Authors
{
    public class Author : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; private set; }
        public DateTime BirthDate { get; set; }
        public string ShortBio { get; set; }

        private Author()
        {
        }

        public Author(
            Guid id,
            [NotNull] string name,
            DateTime birthDate,
            [CanBeNull] string shortBio = null)
            : base(id)
        {
            SetName(name);
            this.BirthDate = birthDate;
            this.ShortBio = shortBio;
        }

        internal Author ChangeName([NotNull] string name)
        {
            this.SetName(name);
            return this;
        }

        private void SetName([NotNull] string name)
        {
            this.Name = Check.NotNullOrWhiteSpace(
                name,
                nameof(Name),
                maxLength: AuthorConsts.MaxNameLength);
        }
    }
}