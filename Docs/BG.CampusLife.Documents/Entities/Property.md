# Property

this models almost contains every attribute we want to set for other entities.
The property that separates this from other entities is [category id].
Let's say we have a category named _Car-Benz-E200_, if our category has some attributes like **Coupe {true:false}**,
we put the **Coupe** in Property entity with ControlType of CheckBox. Let's go for another example, if we have a category named _Apartment_, we have attribute **RoomNumbers{1, 2, 3}**
we insert it to our database in Property Table with ControlType of ComboBox and **Options = "1^2^3"**
the options value will be split into a list for front-end.

[Model](../../../Src/BG.CampusLife.Domain/Entities/Property.cs)
