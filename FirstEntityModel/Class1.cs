
using System;

namespace FirstEntityModel
{
    public class UserEntityModel
    {

         

        public static FirstModel.User EntityToModel(FirstEnity.User userEntity, FirstModel.User userModel)
        {
            if (userModel == null)
            {
                userModel = new FirstModel.User();
            }
            if (userEntity == null)
            {
                throw new Exception("User Entity should not be null");
            }
            userModel.Id = userEntity.Id;
            userModel.LastName = userEntity.LastName;
            userModel.FirstName = userEntity.FirstName;
            userModel.age = userEntity.age;
            userModel.dept = new FirstModel.Department();
                
                DeptEntityModel.EntityToModel(userEntity.Dept, userModel.dept);
            


            return userModel;
        }

        public static FirstModel.User ModelToEntity(FirstModel.User userModel , FirstEnity.User userEntity)
        {
            if (userEntity == null)
            {
                userEntity = new FirstEnity.User();
            }
            if (userModel == null)
            {
                throw new Exception("User Model should not be null");
            }
            userEntity.Id = userModel.Id;
            userEntity.LastName = userModel.LastName;
            userEntity.FirstName = userModel.FirstName;
            userEntity.age = userModel.age;
            return userModel;
        }
    }
}
