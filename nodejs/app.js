const express = require("express");
const path = require("path");
const bodyParser = require("body-parser");
const studRoutes = require("./routes/studentRoutes");
const teachRoutes = require("./routes/teacherRoutes");

const app = express();

const port = 3000;

const mongoose = require("mongoose");
mongoose.connect(
  //"mongodb+srv://root:myroot@cluster0.gq94xxf.mongodb.net/?retryWrites=true&w=majority",
  "mongodb://my-mongodb-2:pohzuIolgWnZv7OPjTnXDoIl06zbd7hVxvqY4Oeof3KJ4OWED97RMDTzDijpJIkMFHsAjXgEvZlHACDb8C58NA%3D%3D@my-mongodb-2.mongo.cosmos.azure.com:10255/?ssl=true&retrywrites=false&maxIdleTimeMS=120000&appName=@my-mongodb-2@",
  {
    useNewUrlParser: true,
    useUnifiedTopology: true,
  }
);
const db = mongoose.connection;
db.on("error", (error) => console.log(error));
db.once("open", () => console.log("connected to database"));

app.set("view engine", "ejs");

app.use(express.static("assets"));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));

//routes
app.use("/student", studRoutes);
app.use("/teacher", teachRoutes);

app.get("/", (req, res) => {
  res.render("index");
});


app.listen(port, () => {
  console.log(`Application is running on http://localhost:${port}`);
});
