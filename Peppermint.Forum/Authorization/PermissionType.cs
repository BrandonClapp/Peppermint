using Peppermint.Core.Authorization;

namespace Peppermint.Forum.Authorization
{
    public class CategoryPermission : PermissionType
    {
        public CategoryPermission(string value) : base(value) { }

        public override string Module { get; } = ModuleSettings.Schema;
        public override string PermissionGroup { get; } = "Category";

        public static CategoryPermission CanViewCategory { get { return new CategoryPermission(nameof(CanViewCategory)); } }
        public static CategoryPermission CanCreateCategory { get { return new CategoryPermission(nameof(CanCreateCategory)); } }
        public static CategoryPermission CanEditCategory { get { return new CategoryPermission(nameof(CanEditCategory)); } }
        public static CategoryPermission CanDeleteCategory { get { return new CategoryPermission(nameof(CanDeleteCategory)); } }

        public static CategoryPermission CanViewPosts { get { return new CategoryPermission(nameof(CanViewPosts)); } }
        public static CategoryPermission CanCreatePosts { get { return new CategoryPermission(nameof(CanCreatePosts)); } }
        public static CategoryPermission CanEditOwnPosts { get { return new CategoryPermission(nameof(CanEditOwnPosts)); } }
        public static CategoryPermission CanEditOthersPosts { get { return new CategoryPermission(nameof(CanEditOwnPosts)); } }
        public static CategoryPermission CanDeleteOwnPosts { get { return new CategoryPermission(nameof(CanDeleteOwnPosts)); } }
        public static CategoryPermission CanDeleteOthersPosts { get { return new CategoryPermission(nameof(CanDeleteOthersPosts)); } }
    }

}
