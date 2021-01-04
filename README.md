# PSO+: A new particle swarm optimization algorithm for constrained problems


### Code Files
This project contains the source code (in C#) described in 'PSO+: A new particle swarm optimization algorithm for constrained problems'. This [work](https://doi.org/10.1016/j.asoc.2019.105865) was published in Applied Soft Computing, Volume 85, December 2019, 105865 ==> [Click](https://doi.org/10.1016/j.asoc.2019.105865)

This repository contains two projects:

1. BaseSystem-develop which contains an implementation of PSO+ without the implementation of neighborhood and metropolis-hastings, but fully working;

This code has no metropolis hastings and no neighborhood implementation, but it is the only one that has a graphical interface and the library call is clearer.


The interface is no big deal, but it helps:
![interface](https://github.com/manoelakohler/PSOplus/blob/main/interface.png)

It's divided into types of optimization, real, integer, minimization, maximization for problems with linear and nonlinear constraints.
You can also run through the interface some benchmarks (which I did not use in my work), but I used to test the pso library that was the basis for building mine (The first two buttons: original and with domain restrictions). In the dropdown menu, the workbook is configured. There, an excel file will be saved with the results (average, best and offline per generation), runtime graphs, optimization parameters, etc ... The modify button you canuse modify these optimization parameters. The "Run Forest Run" button runs everything: it can take a little time. Don't expect anything more from the interface. The original idea was to show results and graphs on it, but time was short, so...

When I implemented neighborhood and metropolis, I already moved on to the well allocation project, which I cannot share the code.
So, inside the second directory, there's the pso+ code, excluding the private part.


2. BaseSystem-develop2 which contains the final version of PSO+, but this version requires some adaptation to run, as it was taken from a private repo that I cannot share publicly. Metropolis Hastings files are not general, so it was implemented based on my final study case: well allocation in oil reservoir.

This code must be called from your program. These classes are loose and must be added to the project to be developed. Your code must also be adapted to the problem to be solved.

### Tutorial

Microsoft Visual Studio 2017 was used. A newer version or any other IDEs will probably need adaptations.

For the intial code:
- Double click .\BaseSystem-develop\SourceCode\NLC_PSO.sln to open solution in VS 2017. 
- Set PSO.View as StartUp Project.
- Run.

### Citation

```
@article{kohler2019,
    title={PSO+: A new particle swarm optimization algorithm for constrained problems},
    author={Kohler, Manoela; Vellasco, Marley M.B.R. and Tanscheit, Ricardo},
    journal={Applied Soft Computing},
    volume={85},
    year={2019},
    publisher={Elsevier}
 }
 ```

### Acknowledgments
The authors would like to thank CNPq, Brazil, FAPERJ, Brazil and Intel Corporation, USA for their financial support. In addition, we would like to thank Petrobras research group from CENPES for their support and advices.

ATTN: This code is free for academic usage. For other purposes, please contact Manoela Kohler (manoela@ele.puc-rio.br).
