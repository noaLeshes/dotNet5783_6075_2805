namespace DO;

public struct Product
{
   public int ID { get; set; }
   public string? Name { get; set; }
   public double Price { get; set; }
    public string? img { get; set; }
    public Category Category { get; set; }
   public int InStock { get; set; }

    public override string ToString()
    {
        return this.ToStringProperty();
    }
}


