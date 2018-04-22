var n, str, start, chunks = [], index, temp, teens, newChunks = [], b = [], answer = [], cents;
var units = ['one', 'two', 'three', 'four', 'five', 'six', 'seven', 'eight', 'nine'];
var teensNum = [11,12,13,14,15,16,17,18,19];
var teensWord = ['eleven', 'twelve', 'thirteen', 'fourteen', 'fifteen', 'sixteen', 'seventeen', 'eighteen', 'nineteen'];
var tens = ['ten', 'twenty', 'thirty', 'forty', 'fifty', 'sixty', 'seventy', 'eighty', 'ninety'];
var scales = ['', 'thousand', 'million'];

n = 100000001.99;

if(n < 1){
  answer.push('Zero dollars');
} else {
  answer.push('dollars');
}

str = n.toString(); // converts the number to a string

if (n % 1 != 0){ // checks to see if cents in the number
  cents = str.slice(str.indexOf('.')); // saves the cents value to a new string
  str = str.replace(cents, ''); // removes the cents from the original string
  
  cents = cents.replace('.', ''); //removes the decimal point from the cents
  
  cents.split('');
  centNum = parseInt(cents);
  if(centNum >= 11 && centNum <= 19){
    for (var j = 0; j < teensNum.length; j++){ // runs through the array of teen nums
      if (centNum === teensNum[j]){ // if the teen from my num matches one of the teens in the array
        b.unshift(teensWord[j]); // push the word to the temp answer array
      }
    }
  }
  else {
    var unitNum = cents.charAt(1); // takes the last num in the str to a new var
    unitNum = parseInt(unitNum); // converts it to a num
    if (unitNum > 0){
      b.unshift(units[unitNum - 1]); // pushes the word to temp answer array
    }
    var tensNum = cents.charAt(0); // takes the second num in the str to a new var for the tens
    tensNum = parseInt(tensNum); // converts it to a num
    if (tensNum > 0 ){
      b.unshift(tens[tensNum - 1]); // pushes the word to temp answer array
    }
  }
  b.unshift('and');
  b.push('cents');
  answer.push(b.join(' ')); // pushes b to the answer  
  b =[];
}

// to make it more manageable, I want to put it in chunks of 3
start = str.length / 3; // divides the length of the string by 3 to know how many chunks I'll have
str = str.split("").reverse().join(""); // reverses the number in preparation to convert it to chunks of 3

for (var i = 0; i < str.length; i+=3) { // loops through the string in sets of 3
  chunks.push(str.substr(i, 3)); // pushes each set to the array
}

// loop to reverse back the order of each array item, but with the sets of three still in the reverse order.
for(var i =0; i<chunks.length; i++){
  temp = chunks[i];
  temp = temp.split('').reverse().join('');
  newChunks.push(temp);
}

for (var i = 0; i < newChunks.length; i++){
  var a = newChunks[i]; // for storing the length of this set
  var q = parseInt(a); // for converting the set back to an integer

  // if statement to add in the scale words - thousand, million, etc
  if (q > 0 && i > 0){
    answer.unshift(scales[i]);
  }
  
  //check for teens
  teens = newChunks[i][a.length-2] + newChunks[i][a.length-1]; // adds the last two nums in the str to a new var
  teens = parseInt(teens); // converts it to a num to compare

  if (teens <= 19 && teens >= 11) { // checking if the var matches a teen num
    for (var j = 0; j < teensNum.length; j++){ // runs through the array of teen nums
      if (teens === teensNum[j]){ // if the teen from my num matches one of the teens in the array
        b.unshift(teensWord[j]); // push the word to the temp answer array
      }
    }
    // next checks for any valid numbers in the hundreds field
    var bigNum = newChunks[i][(a.length-3)];
    bigNum = parseInt(bigNum);
    if(bigNum > 0){
      b.unshift(units[bigNum-1] + ' hundred');
      if (b.length > 1){ // if there are numbers inside this hundred, add an 'and' to make sense
        b.splice(1, 0, "and");
      }
    }
    answer.unshift(b.join(' ')); // pushes b to the answer  
    b = []; // clears the temp array for the next round 
  }
  else if (q > 0) { // if not a teen but the string is greater than 0, now working bakwards through the str to work out the correct two nums
    
    var unitNum = newChunks[i][(a.length-1)]; // takes the last num in the str to a new var
    unitNum = parseInt(unitNum); // converts it to a num
    if (unitNum > 0){
      b.unshift(units[unitNum - 1]); // pushes the word to temp answer array
    }
    
    var tensNum = newChunks[i][(a.length-2)]; // takes the second num in the str to a new var for the tens
    tensNum = parseInt(tensNum); // converts it to a num
    if (tensNum > 0 ){
      b.unshift(tens[tensNum - 1]); // pushes the word to temp answer array
    }
    
    var bigNum = newChunks[i][(a.length-3)];
    bigNum = parseInt(bigNum);
    if (bigNum > 0) { // checks if it's over 0
      b.unshift(units[bigNum-1] + ' hundred'); // pushes the number with hundred to the temp array
      if (b.length > 1){ // if there are other numbers, this adds the and
        b.splice(1, 0, "and");
      }
    }
    
    // check if it's the first item in array, so last in the number, and if less than 100 it will need 'and' in front of it.
    if (i === 0 && q < 100 && newChunks.length > 1){
      b.unshift('and'); 
    }
    
    answer.unshift(b.join(' ')); // pushes b to the answer  
    b = []; // clears the temp array for the next round 
  }
}


console.log(answer.join(' '));