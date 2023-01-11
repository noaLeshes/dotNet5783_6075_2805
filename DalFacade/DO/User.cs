namespace DO;

public struct  User
{
    public string? Name { get; set; }    
    public string? Password { get; set; }
    public string? UserGmail { get; set; }
    public string? Address { get; set; }
    public UserStatus UserStatus { get; set; }

    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
