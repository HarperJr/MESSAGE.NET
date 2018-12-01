using DataService.Data.Models;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DataService.Data {
    public class LocalDbContext : DbContext {

        public LocalDbContext() : base("LocalDb") {

        }

        public DbSet<Consumer> Consumers { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Dialog> Dialogs { get; set; }

        public DbSet<DialogParticipant> DialogParticipants { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Multimedia> Multimedia { get; set; }

        public DbSet<AttachedMultimedia> AttachedMultimedia { get; set; }
    }
}