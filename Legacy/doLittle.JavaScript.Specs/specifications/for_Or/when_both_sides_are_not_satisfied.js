﻿describe("when both sides are not satisfied", function () {
    
    var leftHandSideEvaluator = sinon.stub().returns(false);
    var leftHandSide = Dolittle.specifications.Specification.create()
    leftHandSide.evaluator = leftHandSideEvaluator;

    var rightHandSideEvaluator = sinon.stub().returns(false);
    var rightHandSide = Dolittle.specifications.Specification.create();
    rightHandSide.evaluator = rightHandSideEvaluator;

    var instance = { something: 42 };
    var rule = Dolittle.specifications.Or.create({
        leftHandSide: leftHandSide,
        rightHandSide: rightHandSide
    });
    rule.evaluate(instance);

    var satisfied = rule.isSatisfied();

    it("should not be considered satisfied", function () {
        expect(satisfied).toBe(false);
    });
});