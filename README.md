# component-design-pattern-bug-fix-attempt
continues where the previous left off (CAUSE OF FUCKEN UNITY BUGS)

inital commit after making rebuilding project from ground up: fixed superjump bug by adding some spaghetti to the jumps but its not too bad they just check if eachother is on, plus some states turn them on/off

what was t he bug that made me have to make the project again? unity stopped reading inputs and/or changed the physics unless you toggled disable/re-enable on gameobjects during runtime
the input reading stop + temp fix is happening occasionaly in brand new projects as well still sometimes
