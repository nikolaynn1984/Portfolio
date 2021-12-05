namespace Landing.Library.Model
{
    public class UserRole
    {
        /// <summary>
        /// Роли
        /// </summary>
        /// <param name="Id">Идентификатор</param>
        /// <param name="Name">Название</param>
        public UserRole(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }

        public UserRole()
        {
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get => this.id; set => this.id = value; }
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get => this.name; set => this.name = value; }
        /// <summary>
        /// Поле идентификатор
        /// </summary>
        private int id;
        /// <summary>
        /// Поле Название
        /// </summary>
        private string name;
    }
}
