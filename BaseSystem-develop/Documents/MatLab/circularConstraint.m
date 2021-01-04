function [c,ceq] = circularConstraint(x)

ceq = [];

%radius = 3
c = x(:,1).^2 + x(:,2).^2 - 9;