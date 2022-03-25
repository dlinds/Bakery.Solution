using System.Collections.Generic;

namespace Bakery.Models
{

  public class Treat
  {
    public Treat()
    {
      this.JoinEntities = new HashSet<TreatFlavor>();

    }

    public int TreatId { get; set; }

    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<TreatFlavor> JoinEntities { get; set; }
  }
}
