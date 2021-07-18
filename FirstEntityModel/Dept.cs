
using System;

namespace FirstEntityModel
{
    public class DeptEntityModel
    {

         

        public static FirstModel.Department EntityToModel(FirstEnity.Entity.Department deptEntity, FirstModel.Department deptModel)
        {
            if (deptModel == null)
            {
                deptModel = new FirstModel.Department();
            }
            if (deptEntity == null)
            {
                throw new Exception("dept Entity should not be null");
            }
            deptModel.Id = deptEntity.Id;
            deptModel.Name = deptEntity.Name;
            //deptModel.FirstName = deptEntity.FirstName;
           // deptModel.age = deptEntity.age;
            


            return deptModel;
        }

        public static FirstModel.Department ModelToEntity(FirstModel.Department deptModel , FirstEnity.Entity.Department deptEntity)
        {
            if (deptEntity == null)
            {
                deptEntity = new FirstEnity.Entity.Department();
            }
            if (deptModel == null)
            {
                throw new Exception("dept Model should not be null");
            }
            deptEntity.Id = deptModel.Id;
            deptEntity.Name = deptModel.Name;            
            return deptModel;
        }
    }
}
