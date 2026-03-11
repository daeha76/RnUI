namespace RnUI.Components.UI.RadioGroup;

/// <summary>
/// Context for radio group cascading state.
/// Record type ensures CascadingValue detects changes when SelectedValue differs.
/// </summary>
public record RadioGroupContext(RnRadioGroup Group, string? SelectedValue);
