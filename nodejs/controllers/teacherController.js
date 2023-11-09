const userModel = require("../models/teacherModel");
const studModel = require("../models/studentModel");
const bcrypt = require("bcrypt");
const jwt = require("jsonwebtoken");
const SECRET_KEY = "secretkey";

const login = (req, res) => {
  res.render("teacher/login", { flag: false });
}

const register = (req, res) => {
  res.render("teacher/register", { flaga: false, flagb: false });
}

const signIn = async (req, res) => {
  const { email, password } = req.body;
  try {
    
    const existingUser = await userModel.findOne({ email: email });
    if (!existingUser) {
      res.render("teacher/login", { flag: true });
    }
    
    const matchPassword = await bcrypt.compare(password, existingUser.password);  
    if (!matchPassword) {
      res.render("teacher/login", { flag: true });
    }
    
    const token = jwt.sign({ email: existingUser.email, id: existingUser._id }, SECRET_KEY);
    res.redirect("/teacher/view/?token=" + token);

  } catch (error) {
    console.log(error);
    res.status(500).json({ message: "something went wrong in signin" });
  }
};

const signUp = async (req, res) => {
  const { email, password, cnfpassword } = req.body;
  if (password !== cnfpassword) {
     res.render("teacher/register", { flaga: false, flagb: true });
  }
  try {
    
    const existingUser = await userModel.findOne({ email: email });
    if (existingUser) {
      return res.status(400).json({message: "user already exist"});
    }

    const hashedPassword = await bcrypt.hash(password, 10);
    
    const result = await userModel.create({
      email: email,
      password: hashedPassword
    });
    
    res.render("teacher/register", { flaga: true, flagb: false });

  } catch (error) {
    console.log(error);
    res.status(500).json({ message: "something went wrong" });
  }
};

const viewAll = async (req, res) => {
  const allStudent = await studModel.find();
  res.render("teacher/viewAll", {student : allStudent, token : req.query.token})
};

const add_get = (req, res) => {
  res.render("teacher/add", {token: req.query.token, flag: false});
};

const add_post = async (req, res) => {
  const { name, roll, dob, score, token } = req.body;
  const exist = await studModel.findOne({ roll: roll });
  if (exist) {
    res.render("teacher/add", { token: token, flag: true });
  }
  const student = new studModel({
    name: name,
    roll: roll,
    dob: dob,
    score: score
  })
  try {
    await student.save();
    res.redirect("/teacher/view/?token=" + token);
  } catch (error) {
    console.log(error);
    res.status(500).json({ msg: "Something went worng in addding student" });
  }
};

const edit_get = async (req, res) => {
  const id = req.params.id;
  try {
    const user = await studModel.findById(id);
    const newUser = {
      name: user.name,
      roll: user.roll,
      dob: user.dob.toISOString().slice(0, 10),
      score: user.score
    }
    res.render("teacher/edit", { user: newUser, token: req.query.token });
  } catch (error) {
    console.log(error);
    res.status(500).json({ msg: "Something went worng edit get" });
  }
};

const edit_put = async (req, res) => {
  const id = req.params.id;
   const { name, roll, dob, score, token } = req.body;
   const student = {
     name: name,
     roll: roll,
     dob: dob,
     score: score,
   };
  try {
    await studModel.findByIdAndUpdate(id, student);
    res.redirect("/teacher/view/?token=" + token);
  } catch (error) {
    console.log(error);
    res.status(500).json({ msg: "Something went worng in edit post" });
  }
};

const delete_res = async (req, res) => {
  const id = req.params.id;
  const token = req.query.token;
  try {
    const delData = await studModel.findByIdAndRemove(id);
    res.redirect("/teacher/view/?token=" + token);
  } catch (error) {
    console.log(error);
    res.status(500).json({ msg: "Something went worng" });
  }
};


module.exports = {
  login,
  register,
  signIn,
  signUp,
  viewAll,
  add_get, 
  add_post,
  edit_get,
  edit_put,
  delete_res
};
