// Github link at https://github.com/Overlord-Zurg/NameyAgey.git

open System

[<Literal>]
let EXIT_PHRASE = "DONE!"

let readNameOrExitCommand () =
    do Console.WriteLine("You may enter " + EXIT_PHRASE + " instead of a name to continue to the output.")
    do Console.Write("Please enter a name: ")
    let nameInput = Console.ReadLine()
    if nameInput = EXIT_PHRASE
    then None
    else Some nameInput

let rec readValidAge () =
    do Console.Write("Please enter an age: ")
    let ageInput = Console.ReadLine()
    match Int32.TryParse(ageInput) with
    | false, _ ->
        Console.WriteLine("That is not a valid age.")
        readValidAge ()
    | true, age -> age

type Person = { Name: string
                Age: int }

let rec readPeople peopleSoFar =
    let nameOrExitCommand = readNameOrExitCommand ()
    match nameOrExitCommand with
    | Some name -> // the user entered a name - get a valid age...
        let age = readValidAge ()
        // ...and call this function again with the new person added to the front of our list
        let newPerson = { Name = name; Age = age }
        let newPeopleList = newPerson::peopleSoFar
        // note the tail recursion - the recursive method is the last call in this branch
        readPeople newPeopleList
    | None -> // the user entered an exit command - return the people
              // Note that we return them in reverse order since we added new ones to the front of the list
        List.rev peopleSoFar

let printLifeStage (person: Person) =
    let ageDescription =
        if person.Age >= 20 then
            " is no longer a teenager."
        elif person.Age < 20 && person.Age >= 13 then
            " is a teenager."
        else
            " is a kid or child."
    do Console.WriteLine(person.Name + ageDescription)

[<EntryPoint>]
let main argv = 
    let people = readPeople [] // call the recursive function readPeople, starting with an empty list
    do people |> List.iter printLifeStage // print the life stage for each person in the list
    do Console.ReadKey () |> ignore
    0 // return an integer exit code
