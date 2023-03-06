using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace PM1Reflection;

internal class FurAnimal : INotifyPropertyChanged
{
    public event EventHandler<PropertyChangingExEventArgs>? PropertyChangingEx;

    public class PropertyChangingExEventArgs : EventArgs
    {
        public string PropertyName { get; init; }

        public object? OldValue { get; set; }

        public object? NewValue { get; set; }

        public bool Cancel {  get; set; }

        public PropertyChangingExEventArgs(string propertyName, object? oldValue, object? newValue)
        {
            PropertyName = propertyName;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }

    #region private fields
    private string _name;
    private string? _description;
    private float _weight;
    private float _height;
    private DangerLevel _dangerLevel;
    private CutenessLevel _cutenessLevel;
    #endregion

    #region Properties
    [StringLength(20), Description("Имя"), Required]
    public string Name 
    {
        get => _name;
        set
        {
            if (_name == value)
                return;

            var cancel = OnPropertyChanging(_name, value);
            if (cancel)
                return;

            _name = value;
            OnPropertyChanged();
        }
    }


    public string? Description
    {
        get => _description;
        set
        {
            if (value == _description)
                return;
            _description = value;
            OnPropertyChanged();
        }
    }

    public float Weight
    {
        get => _weight;
        set
        {
            if (value == _weight)
                return;
            _weight = value;
            OnPropertyChanged();
        }
    }

    public float Height
    {
        get => _height;
        set
        {
            if (value == _height)
                return;
            _height = value;
            OnPropertyChanged();
        }
    }

    public DangerLevel DangerLevel
    {
        get => _dangerLevel;
        set
        {
            if (value == _dangerLevel)
                return;
            _dangerLevel = value;
            OnPropertyChanged();
        }
    }

    public CutenessLevel CutenessLevel
    {
        get => _cutenessLevel;
        set
        {
            if (value == _cutenessLevel)
                return;
            _cutenessLevel = value;
            OnPropertyChanged();
        }
    }

    #endregion

    #region public methods
    public void Say()
    {
        Console.WriteLine("bla bla bla");
    }
    #endregion

    #region overriden
    public override string ToString()
    {
        return $"{Name}, {Description}, {EnumUtils.GetDescription(CutenessLevel)}";
    }
    #endregion

    #region INotifyPropertyChanged
    public event PropertyChangedEventHandler? PropertyChanged;
    #endregion

    #region private methods
    private void OnPropertyChanged([CallerMemberName]string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool OnPropertyChanging(object? oldValue, object? newValue, [CallerMemberName] string? propertyName = null)
    {
        var args = new PropertyChangingExEventArgs(propertyName, oldValue, newValue);
        PropertyChangingEx?.Invoke(this, args);
        return args.Cancel;
    }
    #endregion
}
