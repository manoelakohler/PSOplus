problem = createOptimProblem ('fmincon', 'objective', ...
     @(x) funcPeaks(x(1),x(2)), 'nonlcon', @circularConstraint, ...
     'x0', [0 0]);
     %'x0', [0.2283 -1.6255]);
     
%[x1,f1] = fmincon(problem);
gs = GlobalSearch;
[xgPeak, fgPeak, exitflagPeak, outputPeak, solutionsPeak] = run(gs, problem);

%%%%STEP %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
problem = createOptimProblem ('fmincon', 'objective', ...
     @(x) funcStep(x(1),x(2)), 'x0', [0 0]);
     
gs = GlobalSearch;
[xgStep, fgStep, exitflagStep, outputStep, solutionsStep] = run(gs, problem);
 

%%%%ROSENBOCK%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

problem = createOptimProblem ('fmincon', 'objective', ...
     @(x) funcRosenbock(x(1),x(2)), 'x0', [0 0]);
     
gs = GlobalSearch;
[xgRosenbock, fgRosenbock, exitflagRosenbock, outputRosenbock, solutionsRosenbock] = run(gs, problem);

%%%%MANYPEAKS%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

problem = createOptimProblem ('fmincon', 'objective', ...
     @(x) funcManyPeaks(x(1),x(2)), 'nonlcon', @circularConstraint, ...
     'x0', [0 0]);
     
gs = GlobalSearch;
[xgManyPeaks, fgManyPeaks, exitflagManyPeaks, outputManyPeaks, solutionsManyPeaks] = run(gs, problem);

%%%%HOLE%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

problem = createOptimProblem ('fmincon', 'objective', ...
     @(x) funcHole(x(1),x(2)), 'nonlcon', @circularConstraint, ...
     'x0', [0 0]);
     
gs = GlobalSearch;
[xgHole, fgHole, exitflagHole, outputHole, solutionsHole] = run(gs, problem);

%%%%BUMPS%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

problem = createOptimProblem ('fmincon', 'objective', ...
     @(x) funcBumps(x(1),x(2)), 'nonlcon', @circularConstraint, ...
     'x0', [0 0]);
     
gs = GlobalSearch;
[xgBumps, fgBumps, exitflagBumps, outputBumps, solutionsBumps] = run(gs, problem);

%%%%SPHERE%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

problem = createOptimProblem ('fmincon', 'objective', ...
     @(x) funcSphere(x(1),x(2)), 'nonlcon', @circularConstraint, ...
     'x0', [0 0]);
     
gs = GlobalSearch;
[xgSphere, fgSphere, exitflagSphere, outputSphere, solutionsSphere] = run(gs, problem);

%%%%Sinc%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

problem = createOptimProblem ('fmincon', 'objective', ...
     @(x) funcSinc(x(1),x(2)), 'nonlcon', @circularConstraint, ...
     'x0', [0 0]);
     
gs = GlobalSearch;
[xgSinc, fgSinc, exitflagSinc, outputSinc, solutionsSinc] = run(gs, problem);











