var express = require("express");
const teachrouter = express.Router();
const { signIn, signUp, viewAll, edit_get, add_get, add_post, edit_put, delete_res, login, register } = require("../controllers/teacherController");
const auth = require("../middelwares/authorization");

teachrouter.get("/login", login);
teachrouter.get("/register", register)
teachrouter.get("/view", auth, viewAll);
teachrouter.get("/add", auth,  add_get);
teachrouter.get("/edit/:id",  edit_get);
teachrouter.get("/delete/:id", delete_res);

teachrouter.post("/signin", signIn);
teachrouter.post("/signup", signUp);
teachrouter.post("/add", add_post);
teachrouter.post("/edit/:id", edit_put);




module.exports = teachrouter;