using System.Collections.Generic;

namespace Bakery.Models
{

  public class Flavor
  {
    public Flavor()
    {
      this.JoinEntities = new HashSet<TreatFlavor>();
    }
    public int RecipeId { get; set; }


    public virtual ICollection<TreatFlavor> JoinEntities { get; }
    public virtual ApplicationUser User { get; set; }
  }
}
