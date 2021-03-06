﻿describe("when not populated externally", function () {

    var parameters = {
        commandCoordinator: {
        },
        commandValidationService: {
            extendPropertiesWithoutValidation: sinon.stub(),
            getValidatorsFor: sinon.stub(),
            validateSilently: sinon.stub()
        },
        commandSecurityService: {
            getContextFor: function (command) {
                commandAskedForSecurityContext = command;
                return {
                    continueWith: function (callback) {
                        callback("blah");
                    }
                }
            }
        },
        options: {
            name: "something"
        },
        region: {
            commands: []
        },
        mapper: {}
    }
    var command = Dolittle.commands.Command.create(parameters);

    it("should indicate that it is not populated externally", function() {
        expect(command.isPopulatedExternally()).toBe(false);
    });

    it("should be considered ready", function () {
        expect(command.isReady()).toBe(true);
    });

    it("should not have changes", function () {
        expect(command.hasChanges()).toBe(false);
    });

    it("should be considered ready to execute", function () {
        expect(command.isReadyToExecute()).toBe(true);
    });
});
