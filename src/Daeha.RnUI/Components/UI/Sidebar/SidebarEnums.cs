namespace RnUI.Components.UI.Sidebar;

/// <summary>Size variant for the sidebar menu button.</summary>
public enum SidebarMenuButtonSize
{
    Default,
    Sm,
    Lg
}

/// <summary>Visual variant for the sidebar.</summary>
public enum SidebarVariant
{
    Default,
    Floating,
    Inset
}

/// <summary>Collapsible mode for the sidebar.</summary>
public enum SidebarCollapsibleMode
{
    /// <summary>Sidebar slides off-screen when collapsed.</summary>
    Offcanvas,
    /// <summary>Sidebar collapses to icon-only width (3rem).</summary>
    Icon,
    /// <summary>Sidebar is not collapsible.</summary>
    None
}

/// <summary>Which side of the viewport the sidebar appears on.</summary>
public enum SidebarSide
{
    Left,
    Right
}
