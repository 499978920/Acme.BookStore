using Acme.BookStore.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Acme.BookStore.Permissions
{
    public class BookStorePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var bookStoreGroup = context.AddGroup(BookStorePermissions.GroupName, L("Permission:BookStore"));

            var bookPermission = bookStoreGroup.AddPermission(BookStorePermissions.Books.Default, L("Permission:Books"));
            bookPermission.AddChild(BookStorePermissions.Books.Create, L("Permission:Books:Create"));
            bookPermission.AddChild(BookStorePermissions.Books.Edit, L("Permission:Books:Edit"));
            bookPermission.AddChild(BookStorePermissions.Books.Delete, L("Permission:Books:Delete"));
            
            var authorPermission = bookStoreGroup.AddPermission(BookStorePermissions.Authors.Default, L("Permission:Authors"));
            authorPermission.AddChild(BookStorePermissions.Authors.Create, L("Permission:Authors:Create"));
            authorPermission.AddChild(BookStorePermissions.Authors.Edit, L("Permission:Authors:Edit"));
            authorPermission.AddChild(BookStorePermissions.Authors.Delete, L("Permission:Authors:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<BookStoreResource>(name);
        }
    }
}
