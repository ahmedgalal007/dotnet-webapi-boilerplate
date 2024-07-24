//using FSH.WebApi.Infrastructure.Persistence.DynamicSchemas;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace FSH.WebApi.Infrastructure.Persistence.Repository;
//public class DynamicCRUDRepository
//{
//    private readonly DynamicDbContext _dbContext;

//    public DynamicCRUDRepository(DynamicDbContext dbContext, string entityName)
//    {
//        _dbContext = dbContext;

//        var entityType = _dbContext.GetType().GetProperty("entityName").PropertyType.GetGenericArguments().FirstOrDefault();
//        object entityAct = Activator.CreateInstance(entityType); // as LivingThing;

//        // Set other properties dynamically
//        Dictionary<string, int> otherProperties = new Dictionary<string, int>()
//        {
//            { "NumberOfTeeths", 10 },
//            { "RunningSpeed", 50 },
//        };

//        // This will set all the properties which are specific to a class.
//        foreach (var prop in otherProperties.Keys)
//        {
//            int propertyValue = otherProperties[prop];
//            entityAct.GetType().GetProperty(prop).SetValue(entityAct, propertyValue);
//        }

//        _dbContext.Add(entityAct);

//        _dbContext.SaveChanges();
//    }


//    public IQueryable Get (string Id)
//    {
//        var linqQuery = _dbContext.GetType().GetProperty("Animals").GetValue(_dbContext);
//        // var entity = linqQurey.Where(x => x.Id == 1).First();

//    }


//    public object update(object entity)
//    {
//        var linqQuery = _dbContext.GetType().GetProperty("Animals").GetValue(_dbContext) as IQueryable;
//        var entity = linqQurey.AsTracking().Where(x => x.Id == 1).First();
//        entity.Name = "New Name";
//        // ...
//        // ...
//        _dbContext.SaveChanges();
//    }

//    public int Delete(Guid id) {
//        var linqQuery = _dbContext.GetType().GetProperty("Animals").GetValue(_dbContext) as IQueryable;
//        var entity = linqQurey.AsTracking().Where(x => x.Id == id).First();
//        _dbContext.Remove(entity);
//        _dbContext.SaveChanges();
//    }
//}
