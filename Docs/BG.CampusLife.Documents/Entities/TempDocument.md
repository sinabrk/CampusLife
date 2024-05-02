# TempDocument

before setting a document to other entity, we save it into our database
so for further actions we store a temp entity in this table, after we are done with setting relations we remove the temp document.

We first upload the file in server, then store the fileId in front,
when we gonna submit post or market item, we send the fileId. Then the real Document Entity will we created and matched to entity.

[Model](../../../Src/BG.CampusLife.Domain/Entities/TempDocument.cs)


