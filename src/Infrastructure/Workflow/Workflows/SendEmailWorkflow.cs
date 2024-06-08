using Elsa.Http;
using Elsa.Workflows.Contracts;
using Elsa.Workflows;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elsa.Workflows.Activities;
using Elsa.Email.Activities;

namespace FSH.WebApi.Infrastructure.Workflow.Workflows;

public class SendEmailWorkflow : WorkflowBase
{
    protected override void Build(IWorkflowBuilder builder)
    {
        builder.Root = new Sequence
        {
            Activities =
            {
                new HttpEndpoint
                {
                    Path = new("/send-email-cls"),
                    SupportedMethods = new(new[] { HttpMethods.Post }),
                    CanStartWorkflow = true
                },
                new SendEmail
                {
                    From = new("alic@acme.com"),
                    To = new(new[]{ "bob@acme.com" }),
                    Subject = new("Your workflow has been triggered!"),
                    Body = new("Hello!")
                }
            }
        };
    }
}