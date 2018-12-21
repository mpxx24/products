using FluentNHibernate.Mapping;

namespace MOVO.Products.Contracts.Products {
    public class ProductMap : ClassMap<Product> {
        public ProductMap() {
            this.Table("tblProducts");
            this.Id(x => x.Id).Column("uId");
            this.Map(x => x.ArticleNumber).Column("vArticleNumber");
            this.Map(x => x.ArticleName).Column("vArticleName");
            this.Map(x => x.Description).Column("vDescription");
            this.Map(x => x.Category).Column("iCategory");
            this.Map(x => x.Price).Column("dPrice");
        }
    }
}