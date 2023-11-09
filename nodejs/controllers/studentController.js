const studModel = require("../models/studentModel");

const search = (req, res) => {
  res.render("student/search", { flag: false });
};

const viewresult = async (req, res) => {

  const { roll, dob } = req.body;
  const result = await studModel.findOne({ roll: roll, dob: dob });

  if (!result) {
    res.render("student/search", { flag: true });
  } else {
    let newDob = result.dob.toDateString();
    const newResult = {
      roll: result.roll,
      dob: newDob,
      score: result.score,
    };
    res.render("student/show", { stuResult: newResult });
  }
  
}

module.exports = {
  search,
  viewresult
};
