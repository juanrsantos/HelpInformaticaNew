namespace Inspinia_MVC5_SeedProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PagamentoMensalidade", "DataPagamento", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PagamentoMensalidade", "DataPagamento");
        }
    }
}
